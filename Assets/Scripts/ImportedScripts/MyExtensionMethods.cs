using UnityEngine;

namespace ImportedScripts
{
    public static class MyExtensionMethods
    {
        #region Animator Extensions
        /// <summary>
        /// Check if the animator has this parameter. Returns false if the animator is null.
        /// </summary>
        /// <param name="animator">This animator reference</param>
        /// <param name="paramName">Parameter string name</param>
        /// <returns></returns>
        public static bool HasParameter(this Animator animator, string paramName)
        {
            if (animator == null) return false;
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.name == paramName) return true;
            }

            return false;
        }

        /// <summary>
        /// Check if the animator has this parameter. Returns false if the animator is null.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="paramNameHash"></param>
        /// <returns></returns>
        public static bool HasParameter(this Animator animator, int paramNameHash)
        {
            if (animator == null) return false;
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.nameHash == paramNameHash) return true;
            }

            return false;
        }

        /// <summary>
        /// Safely checks if the animator is not null, and if it has the parameter passed. Returns true if the parameter was set successfully.
        /// </summary>
        public static bool SafeSetParameter(this Animator animator, int paramHash)
        {
            if (animator.HasParameter(paramHash) == false) return false;

            animator.SetTrigger(paramHash);
            return true;
        }

        public static bool SafeSetParameter(this Animator animator, int paramHash, bool value)
        {
            if (animator.HasParameter(paramHash) == false) return false;

            animator.SetBool(paramHash, value);
            return true;
        }

        public static bool SafeSetParameter(this Animator animator, int paramHash, int value)
        {
            if (animator.HasParameter(paramHash) == false) return false;

            animator.SetInteger(paramHash, value);
            return true;
        }

        public static bool SafeSetParameter(this Animator animator, int paramHash, float value)
        {
            if (animator.HasParameter(paramHash) == false) return false;

            animator.SetFloat(paramHash, value);
            return true;
        }
        #endregion
    }
}